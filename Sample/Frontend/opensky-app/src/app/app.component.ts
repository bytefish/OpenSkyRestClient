import { Component, OnDestroy, OnInit } from '@angular/core';
import { LngLat, LngLatLike, Style } from 'mapbox-gl';
import { Observable, Subject, Subscription } from 'rxjs';
import { filter, map, takeUntil } from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { StateVectorResponse } from './model/state-vector';
import { MapService } from './services/map.service';
import { SseService } from './services/sse.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {

  private readonly destroy$ = new Subject();

  mapZoom: number;
  mapStyle: string;
  mapCenter: LngLatLike;
  isMapLoaded: boolean;

  constructor(private sseService: SseService, private mapService: MapService) {
    this.mapStyle = "http://localhost:9000/static/style/osm_liberty/osm_liberty.json";
    this.mapCenter = new LngLat(7.628202, 51.961563);
    this.mapZoom = 14;
  }

  ngOnInit(): void {

    this.mapService.onMapLoaded()
      .pipe(takeUntil(this.destroy$))
      .subscribe((value) => {
        this.isMapLoaded = value;
      });

    this.sseService
      .asObservable(environment.apiUrl)
      .pipe(
        takeUntil(this.destroy$),
        map(x => <StateVectorResponse>JSON.parse(x.data)))
      .subscribe(x => this.updateStateVectors(x));

  }

  updateStateVectors(stateVectorResponse: StateVectorResponse): void {
    if (this.isMapLoaded) {
      console.log("Updating Map ...");

      this.mapService.displayStateVectors(stateVectorResponse.states);
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
