import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';


import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { TaskComponent } from './components/task/task.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppHttpInterceptor } from './app.http.interceptor';
import { AppRoutingModule } from './app.routing';
import { AuthService } from './services/auth.service';
import { TaskService } from './services/task.services';
import { TaskFormComponent } from './components/task-form/task-form.component';
import { AppGuard } from './app.guard';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    TaskComponent,
    TaskFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AppHttpInterceptor,
      multi: true,
    },
    AuthService,
    TaskService,
    AppGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
