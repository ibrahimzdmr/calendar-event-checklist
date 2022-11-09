import { Actions, createEffect, ofType} from '@ngrx/effects';
import { Injectable } from '@angular/core';
import { catchError, map, of, switchMap } from 'rxjs';
import { loadEvents, loadEventsError, loadEventsSuccess } from '../actions/event.actions';
import { EventService } from 'src/app/services/event.service';
import { EventItem } from 'src/app/models/event-item';

@Injectable({
    providedIn: "root"
  })
export class EventEffects {
    loadEvents$ = createEffect((): any => {
        return this.actions$.pipe(
            ofType(loadEvents),
            switchMap(() => this.eventService.getAllEvents().pipe(
                map((events: EventItem[]) => loadEventsSuccess({events:events})),
                catchError(() => of(loadEventsError))
            ))
        )
    })
  constructor(
    private actions$: Actions,
    private eventService: EventService
  ) {}
}