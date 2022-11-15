import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { NgxSmartModalService } from 'ngx-smart-modal';
import { SubscriptionLike, tap } from 'rxjs';
import { EventItem } from 'src/app/models/event-item';
import { EventService } from 'src/app/services/event.service';
import { loadEvents } from 'src/app/store/actions/event.actions';
import { State } from 'src/app/store/reducers/event.reducer';

@Component({
  selector: 'app-calendar-event-page',
  templateUrl: './calendar-event-page.component.html',
  styleUrls: ['./calendar-event-page.component.css']
})
export class CalendarEventPageComponent implements OnDestroy {

  subscribes: SubscriptionLike[] = []; 
  events!: EventItem[];

  constructor(readonly store: Store<{ state: State }>,
    readonly eventService: EventService,
    readonly ngxSmartModalService: NgxSmartModalService) { 
    this.subscribes.push(this.store.select(i => i.state.events).subscribe((events) => {
      if(events)
        this.events = events
    }));
    this.controlEvents();
    setInterval(() => { this.controlEvents() }, 30000 );
  }

  ngOnDestroy(): void {
    while (this.subscribes.length > 0) {
      this.subscribes.pop()?.unsubscribe();
    }
  }

  controlEvents(): void {
    const sub = this.eventService.controlEvents().subscribe(() => {
      this.store.dispatch(loadEvents());
      sub.unsubscribe();
    })
  }

  showModal(eventId: string) {
    this.ngxSmartModalService.getModal('eventModal').open();
  }

}
