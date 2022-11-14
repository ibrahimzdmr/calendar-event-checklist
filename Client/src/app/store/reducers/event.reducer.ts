import { Action, createReducer, on} from '@ngrx/store';
import { EventItem } from 'src/app/models/event-item';
import { loadEventsSuccess } from '../actions/event.actions';

export interface State {
  events: EventItem[];
}

export const initialState: State = {
    events: []
};

const _storeReducer = createReducer(
    initialState,
    on(loadEventsSuccess, (state: State, payload: any) => {
        return ({...state, events: payload.events});
    }),
  );

export function storeReducer(state : State | undefined, action : Action) {
    return _storeReducer(state, action);
  }