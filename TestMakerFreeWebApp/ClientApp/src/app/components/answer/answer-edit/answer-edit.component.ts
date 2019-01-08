
//DÜSSELDORF UNTERRATH Am Klosterhof Haus St.Josef
import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http'; 

@Component({
  selector: 'answer-edit',
  templateUrl: './answer-edit.component.html',
  styleUrls: ['./answer-edit.component.css']
})
export class AnswerEditComponent implements OnInit {

  answer: IAnswer;
  title: string;
  editMode: boolean;
  id: number;

  constructor(private http: HttpClient,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    @Inject('BASE_URL') private baseUrl: string) {

    this.id = +this.activatedRoute.snapshot.params['id'];
    this.answer = <IAnswer>{};
    this.editMode = (this.activatedRoute.snapshot.url[1].path === 'edit');
    if (this.editMode) {
      this.http.get<IAnswer>(`${this.baseUrl}api/answer/${this.id}`)
        .subscribe(
        result => {
          var v = result;
          this.answer = v;
          this.title = `Editting answer id [${ v.AnswerId }]`;
        },
        error => console.log(error)
        );
    } else {
      this.title = `Create new answer.`;
      this.answer.QuestionId = this.id;
    }
  }

  onSubmit(answer: IAnswer) {
    console.log(answer)
    if (this.editMode) {
      this.http.post<IAnswer>(`${this.baseUrl}api/answer`, answer)
        .subscribe(
          result => {
            var v = result;
            console.log(`Question id [${v.QuestionId}] has been updated.`);
            this.router.navigate([`question/edit`, v.QuestionId]);
          },
          error => console.log(error)
        );
    } else {
      this.http.put<IAnswer>(`${this.baseUrl}api/answer`, answer)
        .subscribe(
          result => {
            var v = result;
            this.router.navigate([`question/edit`, v.QuestionId]);
          },
          error => console.log(error)
        );
    }
  }

  onBack() {
    this.router.navigate([`question/edit`, this.answer.QuestionId]);
  }

  ngOnInit() {
  }

}
