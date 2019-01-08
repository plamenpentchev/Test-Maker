import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

import { IQuestion } from '../../../interfaces/IQuestion';

@Component({
  selector: 'question-edit',
  templateUrl: './question-edit.component.html',
  styleUrls: ['./question-edit.component.css']
})
export class QuestionEditComponent implements OnInit {

  question: IQuestion;
  editMode: boolean;
  id: number;
  title: string;

  constructor(private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private activatedRoute: ActivatedRoute,
        private router: Router)
  {
    this.title = '';
    this.editMode = (this.activatedRoute.snapshot.url[1].path == 'edit');
    this.question = <IQuestion>{};
    this.id = +this.activatedRoute.snapshot.params['id'];
    if (this.editMode) {
      this.http.get<IQuestion>(`${this.baseUrl}api/question/${this.id}`)
        .subscribe(
          result => {
            this.question = result;
            this.title = `Editting ${this.question.Text}[${this.id}]`;
          },
          error => console.log(error)
        );
    } else {
      this.title = `Create new question`;
      this.question.QuizId = this.id;
    }
  }

  onSubmit(question: IQuestion) {
  
    if (this.editMode) {
      this.http.post<IQuestion>(`${this.baseUrl}api/question`, question)
        .subscribe(
        result => {
          var v = result;
          console.log(`Question id [${v.QuestionId}] has been updated.`);
          this.router.navigate([`quiz/edit`, v.QuizId]);
        },
          error => console.log(error)
        );
    } else {
      this.http.put<IQuestion>(`${this.baseUrl}api/question/`, question)
        .subscribe(
        result => {
          var v = result;
          this.router.navigate([`quiz/edit`, v.QuizId]);
        },
          error => console.log(error)
        );
    }
  }

  onBack() {
    this.router.navigate([`quiz/edit`, this.question.QuizId]);
  }

  ngOnInit() {
  }

}
