import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { serviceUrlCombiner } from "../constants";
import { EventItem } from "../models/event-item";

@Injectable({
    providedIn: "root"
  })
  export class EventService {
    constructor(private readonly httpClient: HttpClient) {}

    public getAllEvents() : Observable<EventItem[]> {
        return this.httpClient.get<EventItem[]>(serviceUrlCombiner('Event'));
    }

    public controlEvents() {
      return this.httpClient.post(serviceUrlCombiner('EventFrequencyCalculation', 'Control'), null);
    }
  }