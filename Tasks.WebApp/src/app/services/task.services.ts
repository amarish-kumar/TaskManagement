import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class TaskService {

  AssignTask(data: CreateTaskModel): any {
    return this.httpClient.post('/tasks/assign', data);
  }
  CompleteTask(taskId: string): any {
    return this.httpClient.post('/tasks/complete/'+taskId,{});
  }
  UpdateTask(data: CreateTaskModel): any {
    return this.httpClient.post('/tasks/update', data);
  }
  GetTaskDetails(taskId: string): Observable<CreateTaskModel> {
    return this.httpClient.get<CreateTaskModel>('/tasks/gettask/'+taskId);
  }
  DeleteTask(taskId: string): any {
    return this.httpClient.post('/tasks/delete/'+taskId,{});
  }
  GetMyTaskList(): Observable<TaskRecordModel[]> {
    return this.httpClient.get<any>('/tasks/getmytasks');
  }
  GetUserList() {
    return this.httpClient.get<any>('/tasks/getuserlist');
  }
  constructor(private httpClient: HttpClient) { }

  GetTaskList(data: LoginModel): Observable<CreateTaskModel> {
    return this.httpClient.get<any>('/tasks/getall');
  }

  CreateTask(data: CreateTaskModel) {
    return this.httpClient.post('/tasks/create', data);
  }
}
