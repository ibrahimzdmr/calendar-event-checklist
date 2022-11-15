import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Frequency } from 'src/app/enums/frequency';
import { EventItem } from 'src/app/models/event-item';

@Component({
  selector: 'app-event-card',
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.css']
})
export class EventCardComponent implements OnInit {

  @Input()
  event!: EventItem;

  @Output()
  updateClicked: EventEmitter<string> = new EventEmitter<string>();

  remainingTime!: string;

  constructor() { 
    setInterval(() => { this.calculateRemainingTime() }, 1000)
  }

  ngOnInit(): void {
    this.calculateRemainingTime();
  }

  public calculateRemainingTime() {
    const repeatDate = new Date(this.event.nextRepeatDate);
    const diff = repeatDate.getTime() - new Date().newUTCDate().getTime();
    this.remainingTime = this.ticksToDateString(diff);
  }

  public getFrequencyName(frequency: Frequency) : string{
    switch (frequency) {
      case Frequency.Daily:
        return "Daily";
      case Frequency.Weekly:
        return "Weekly";
      case Frequency.Fortnight:
        return "Fortnight";
      case Frequency.Monthly:
        return "Monthly";
      case Frequency.Yearly:
        return "Yearly";
      default:
        return "";
    }
  }

  updateClick() {
    this.updateClicked.emit(this.event.id);
  }

  private ticksToDateString(ms: number): string{
    const days = Math.floor(ms / (24*60*60*1000));
    const daysms = ms % (24*60*60*1000);
    const hours = Math.floor(daysms / (60*60*1000));
    const hoursms = ms % (60*60*1000);
    const minutes = Math.floor(hoursms / (60*1000));
    const minutesms = ms % (60*1000);
    const sec = Math.floor(minutesms / 1000);
    if(days > 0)
      return days + " days " + hours + ":" + minutes + ":" + sec;
    else
      return hours + ":" + minutes + ":" + sec;
}

}
