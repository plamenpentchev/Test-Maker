import { Component, OnInit, Inject, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { IQuestion } from '../../../interfaces/IQuestion';

@Component({
  selector: 'answer-list',
  templateUrl: './answer-list.component.html',
  styleUrls: ['./answer-list.component.css']
})
export class AnswerListComponent implements OnInit {
  @Input() question: IQuestion;
  answers: IAnswer[];
  title: string;

  constructor(
      private http: HttpClient,
      @Inject('BASE_URL') private baseUrl: string,
      private router: Router
    )
  {
    this.answers = [];
    this.question = <IQuestion>{};
  }

  ngOnChanges(changes: SimpleChanges) {
    
    if (typeof changes['question'] !== "undefined") {
      
      var change = changes['question'];
      if (!change.isFirstChange()) {
        this.loadData();
      }
    }
  }

  loadData() {
    this.http.get<IAnswer[]>(`${this.baseUrl}api/answer/all/${this.question.QuestionId}`)
      .subscribe(
        result => {
          this.answers = result;
        },
        error => console.error(error));
  }

  onCreate() {
    this.router.navigate(["/answer/create", this.question.QuestionId]);
  }

  onEdit(answer: IAnswer) {
    console.log(answer);
    this.router.navigate(["/answer/edit", answer.AnswerId]);
  }

  onDelete(answer: IAnswer) {
    if (confirm('Do you really want to delete this answer?')) {
      this.http.delete(`${this.baseUrl}api/answer/${answer.AnswerId}`)
        .subscribe(
          result => {
            console.log(`Answer mit der id [${answer.AnswerId}] succsessfully deleted`);
            this.loadData();
          },
          error => console.error(error)
        );
    }
  }

  ngOnInit() {
  }

}
