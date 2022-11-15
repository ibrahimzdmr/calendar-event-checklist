import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgxSmartModalModule } from 'ngx-smart-modal';
import { CalendarEventPageComponent } from './calendar-event-page.component';
import { EventCardComponent } from './components/event-card/event-card.component';
import { EventModalComponent } from './components/event-modal/event-modal.component';

@NgModule({
  declarations: [
    CalendarEventPageComponent,
    EventCardComponent,
    EventModalComponent,
  ],
  imports: [
    RouterModule.forChild([{
      path: '',
      pathMatch: 'full',
      component: CalendarEventPageComponent
    }]),
    CommonModule,
    NgxSmartModalModule
  ],
  providers: [],
})
export class CalendarEventModule { }