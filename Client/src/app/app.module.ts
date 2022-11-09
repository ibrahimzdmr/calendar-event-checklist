import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { storeReducer } from './store/reducers/event.reducer';
import { EventEffects } from './store/effects/event.effects';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    StoreModule.forRoot({ state : storeReducer }),
    EffectsModule.forRoot([EventEffects]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
