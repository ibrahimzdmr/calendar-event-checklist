import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { SubscriptionLike } from 'rxjs';
import { EventItem } from 'src/app/models/event-item';
import { loadEvents } from 'src/app/store/actions/event.actions';
import { State } from 'src/app/store/reducers/event.reducer';

@Component({
  selector: 'app-calendar-event-page',
  templateUrl: './calendar-event-page.component.html',
  styleUrls: ['./calendar-event-page.component.css']
})
export class CalendarEventPageComponent implements OnInit, OnDestroy {

  subscribes: SubscriptionLike[] = []; 
  events!: EventItem[];

  constructor(readonly store: Store<{ state: State }>) { 
    this.store.dispatch(loadEvents());
    this.subscribes.push(this.store.select(i => i.state.events).subscribe((events) => this.events = events));
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {
    while (this.subscribes.length > 0) {
      this.subscribes.pop()?.unsubscribe();
    }
  }

}
