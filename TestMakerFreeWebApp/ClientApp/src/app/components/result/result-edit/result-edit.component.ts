import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';



@Component({
  selector: 'result-edit',
  templateUrl: './result-edit.component.html',
  styleUrls: ['./result-edit.component.css']
})
export class ResultEditComponent implements OnInit {

  result: IResult;
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
    this.result = <IResult>{};
    this.id = +this.activatedRoute.snapshot.params['id'];
    if (this.editMode) {
      this.title = `Editting ${this.result.Text}`;
      this.http.get<IResult>(`${this.baseUrl}api/result/${this.id}`)
        .subscribe(
          result => {
            this.result = result;
          },
          error => console.log(error)
        );
    } else {
      this.title = `Create new result`;
      this.result.QuizId = this.id;
    }
  }

  onSubmit(result: IResult) {
    if (this.editMode) {
      this.http.post<IResult>(`${this.baseUrl}api/result`, result)
        .subscribe(
          result => {
            var v = result;
            console.log(`Result id [${v.ResultId}] has been updated.`);
            this.router.navigate([`quiz/edit`, v.QuizId]);
          },
          error => console.log(error)
        );
    } else {
      this.http.put<IResult>(`${this.baseUrl}api/result`, result)
        .subscribe(
          result => {
            var v = result;
            console.log("Result successfully created.");
            this.router.navigate([`quiz/edit`, v.QuizId]);
          },
          error => console.log(error)
        );
    }
  }

  onBack() {
    this.router.navigate([`quiz/edit`, this.result.QuizId]);
  }

  ngOnInit() {
  }

}

