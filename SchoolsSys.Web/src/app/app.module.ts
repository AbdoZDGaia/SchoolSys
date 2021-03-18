import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { StudentsComponent } from './components/students/students.component';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MapComponent } from './components/shared/map/map.component';
import { UploadComponent } from './components/shared/upload/upload.component';
import { DigitOnlyModule } from '@uiowa/digit-only';
import { AngularFileUploaderModule } from "angular-file-uploader";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'


@NgModule({
  declarations: [
    AppComponent,
    StudentsComponent,
    MapComponent,
    UploadComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    BrowserAnimationsModule,
    AngularFileUploaderModule,
    ToastrModule.forRoot(),
    DigitOnlyModule,
    FontAwesomeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
