import { createAction, props } from "@ngrx/store";
import { EventItem } from "src/app/models/event-item";

export const loadEvents = createAction('[Calendar Event Screen] Get Events');
export const loadEventsSuccess = createAction('[Calendar Event Screen] Get Events Success', props<{events: EventItem[]}>());
export const loadEventsError = createAction('[Calendar Event Screen] Get Events Error');