import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css']
})
export class QuizComponent implements OnInit {

 quiz: IQuiz;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string)
  {
    //... create an empty object of IQuiz type
    this.quiz = <IQuiz>{};

    var id = +this.activatedRoute.snapshot.params["id"];
    console.log(`ID:  ${id}`);
    if (id) {
      //... TODO load the quiz using server-side API.
      this.http.get<IQuiz>(`${this.baseUrl}/api/quiz/${id}`).subscribe(
        result => {
          this.quiz = result;
        },
        error => console.error(error)
      );

    }
    else {
      console.log("Invalid id: routing back home");
      this.router.navigate(["home"]);
    }


  }

  ngOnInit() {
  }

}
