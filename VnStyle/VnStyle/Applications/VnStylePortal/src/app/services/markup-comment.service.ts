import { Injectable } from '@angular/core';
import * as firebase from "firebase";
import { Observable } from 'rxjs/Observable';
import { SettingsService } from './settings.service';
import 'rxjs/add/observable/of';
import 'rxjs/Rx';

@Injectable()
export class MarkupCommentService {

  constructor() {
    console.log("MarkupCommentService constructor");
    var config = {
      apiKey: "AIzaSyAQzaF18ITgAYtnK8hFpuV7UeLTaEnXD6U",
      authDomain: "reference-tide-137210.firebaseapp.com",
      databaseURL: "https://reference-tide-137210.firebaseio.com",
      projectId: "reference-tide-137210",
      storageBucket: "reference-tide-137210.appspot.com",
      messagingSenderId: "338378071430"
    };
    firebase.initializeApp(config);
  }

  getComments(markupId, resultCallback) {
    firebase.database().ref(`markup/${markupId}/comments`).on("value", (snapshot) => {
      console.log("snapshot.val()", snapshot.val());

      const comments = snapshot.val();
      var _commentResult = [];
      for (var key in comments) {
        _commentResult.push(Object.assign({ key: key }, comments[key]))
      }

      if (resultCallback && typeof (resultCallback) == 'function') resultCallback(markupId, _commentResult);
    });

  }

  postComment(markupId, comment) {
    // firebase.database().ref(`markup/${markupId}/comments`).orderByKey().startAt("2").limitToFirst(3).on("value", (snapshot) => {
    //   if (resultCallback && typeof (resultCallback) == 'function') resultCallback(markupId, snapshot.val());
    // });
    const currentDate = (new Date());
    const commentId = (+ currentDate);
    comment.createdDate = currentDate.toISOString();
    var promise = firebase.database().ref(`markup/${markupId}/comments/${commentId}`).set(comment);
    return promise;
    //return PromiseObservable.create(promise);

    //return Observable.fromPromise(promise);
  }

}
