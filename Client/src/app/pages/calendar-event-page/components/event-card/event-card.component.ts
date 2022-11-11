import { Component, Input, OnInit } from '@angular/core';
import { EventItem } from 'src/app/models/event-item';

@Component({
  selector: 'app-event-card',
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.css']
})
export class EventCardComponent implements OnInit {

  @Input()
  event!: EventItem;

  constructor() { }

  ngOnInit(): void {
  }

}
