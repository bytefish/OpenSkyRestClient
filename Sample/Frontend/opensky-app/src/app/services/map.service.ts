import { Injectable, NgZone } from "@angular/core";
import * as mapboxgl from 'mapbox-gl';
import { LngLatLike, MapboxOptions, GeoJSONSource, Style } from 'mapbox-gl';
import { BehaviorSubject, Observable } from "rxjs";
import { first } from 'rxjs/operators';
import { StateVector } from '../model/state-vector';

@Injectable({
    providedIn: 'root',
})
export class MapService {

    public mapInstance: mapboxgl.Map;

    private mapCreated$: BehaviorSubject<boolean>;
    private mapLoaded$: BehaviorSubject<boolean>;
    private markers: GeoJSON.FeatureCollection<GeoJSON.Geometry>;

    constructor(private ngZone: NgZone) {
        this.mapCreated$ = new BehaviorSubject<boolean>(false);
        this.mapLoaded$ = new BehaviorSubject<boolean>(false);
        this.markers = {
            type: 'FeatureCollection',
            features: [],
        };
    }

    buildMap(mapContainer: string | HTMLElement, style?: Style | string, center?: LngLatLike, zoom?: number) {
        this.ngZone.onStable.pipe(first()).subscribe(() => {
            this.createMap(mapContainer, style, center, zoom);
            this.registerEvents();
        });

    }

    private createMap(mapContainer: string | HTMLElement, style?: Style | string, center?: LngLatLike, zoom?: number): void {
        const mapboxOptions: MapboxOptions = {
            container: mapContainer,
            style: style,
            center: center,
            zoom: zoom
        };

        this.mapInstance = new mapboxgl.Map(mapboxOptions);
    }

    private registerEvents(): void {
        this.mapInstance.on('load', () => {
            this.mapLoaded$.next(true);
        });

        this.mapInstance.on('style.load', () => {
            // We cannot reference the mapInstance in the callback, so store
            // it temporarily here:
            const map = this.mapInstance;
            const markers = this.markers;
            // We want a custom icon for the GeoJSON Points, so we need to load 
            // an image like described here: https://docs.mapbox.com/mapbox-gl-js/example/add-image/
            map.loadImage('http://localhost:4200/assets/plane.png', function (error, image) {

                if (error) {
                    console.log("There was an error...", error)
                    throw error;
                }

                map.addImage("icon_plane", image);

                // Taken from: https://bl.ocks.org/ryanbaumann/9b9b52e09ff86d1ce8346fb76b681427
                map.addSource('markers', {
                    "type": "geojson",
                    "data": markers
                });

                map.addLayer({
                    "id": "markers",
                    "source": "markers",
                    "type": "symbol",
                    "layout": {
                        "icon-image": "icon_plane",
                    }
                });
            });
        });
    }

    onMapLoaded(): Observable<boolean> {
        return this.mapLoaded$.asObservable();
    }

    onMapCreated(): Observable<boolean> {
        return this.mapCreated$.asObservable();
    }

    displayStateVectors(states: Array<StateVector>): void {
        if (this.mapInstance) {
            this.markers.features = states
                .filter(state => state.longitude && state.latitude)
                .map(state => this.convertStateVectorToGeoJson(state));

            const source: GeoJSONSource = <GeoJSONSource>this.mapInstance.getSource('markers');

            source.setData(this.markers);
        }

    }

    private convertStateVectorToGeoJson(stateVector: StateVector): GeoJSON.Feature<GeoJSON.Point> {
        return {
            type: 'Feature',
            properties: {

                'iconSize': [60, 60]
            },
            geometry: {
                type: 'Point',
                coordinates: [stateVector.longitude, stateVector.latitude]
            }
        };
    }

    destroyMap() {
        console.log("Destroying Map ...");
        if (this.mapInstance) {
            this.mapInstance.remove();
        }
    }
}