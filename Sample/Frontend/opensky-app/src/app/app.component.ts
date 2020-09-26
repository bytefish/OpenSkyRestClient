import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { StateVectorResponse } from './model/state-vector';
import { SseService } from './services/sse.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {

  private readonly destroy$ = new Subject();
  
  stateVector$: Observable<StateVectorResponse>;
  
  constructor(private sseService: SseService) {
  }

  ngOnInit(): void {
    this.sseService
      .asObservable(environment.apiUrl)
      .pipe(
        takeUntil(this.destroy$),
        map(x => <StateVectorResponse>JSON.parse(x.data))
      )
      .subscribe(stateVectorResponse => this.updateStateVectors(stateVectorResponse));
  }

  updateStateVectors(stateVectorResponse: StateVectorResponse): void {
    console.log(stateVectorResponse);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
