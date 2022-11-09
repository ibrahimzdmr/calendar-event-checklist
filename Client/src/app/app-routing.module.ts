import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path:'',
    redirectTo:'/Event-Calendar',
    pathMatch:'full'
  },
  {
    path: 'Event-Calendar',
    loadChildren: () => import('./pages/calendar-event-page/calendar-event-page.module').then(m => m.CalendarEventModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
