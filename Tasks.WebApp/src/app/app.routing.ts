import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { TaskComponent } from './components/task/task.component';
import { TaskFormComponent } from './components/task-form/task-form.component';
import { AppGuard } from './app.guard';

const routes: Routes = [
  {path: '', redirectTo:'login' ,pathMatch:'full'},
  {path: 'login', component: LoginComponent},
  {path: 'tasks', component: TaskComponent,canActivate : [AppGuard]},
  {path: 'tasks/new', component: TaskFormComponent,canActivate : [AppGuard]},
  {path: 'tasks/edit/:taskId', component: TaskFormComponent,canActivate : [AppGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{useHash:true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
