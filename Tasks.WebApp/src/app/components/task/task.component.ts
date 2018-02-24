import { Component, OnInit } from '@angular/core';
import { TaskService } from '../../services/task.services';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

  currentUser: UserInfoModel;
  constructor(private taskService:TaskService,private authService:AuthService) { }
 tasks : TaskRecordModel[];
  ngOnInit() {
    this.taskService.GetMyTaskList().subscribe(res => {

      this.tasks = res;
    }, err => {
      console.log(err);
    });
    this.authService.getProfile().subscribe(res=>{
      this.currentUser = res;
    });
  }

  OnClickDelete(taskId:string)
  {
    if(confirm('are you sure that you want to delete this task?'))
    {
      this.taskService.DeleteTask(taskId).subscribe(res => {
        this.tasks = this.tasks.filter(item => item.Id !== taskId);

      }, err => {
        console.log(err);
      });
    }
  }


  OnClickComplete(task:TaskRecordModel)
  {
    if(confirm('are you sure that you want to complete this task?'))
    {
      this.taskService.CompleteTask(task.Id).subscribe(res => {
       // this.tasks = this.tasks.filter(item => item.Id !== taskId);
        task.Status = 'Complete';
      }, err => {
        console.log(err);
      });
    }
  }

}
