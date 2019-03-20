import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatToolbarModule} from '@angular/material/toolbar';
import { AppComponent } from './app.component';
import { NoteComponent } from './note/note.component';
import { ApiBuilderService } from 'src/app/services/api-builder.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    NoteComponent
  ],
  imports: [
    BrowserModule,
    MatGridListModule,
    MatToolbarModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [ApiBuilderService],
  bootstrap: [AppComponent]
})
export class AppModule { }
