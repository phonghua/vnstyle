/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MarkupCommentService } from './markup-comment.service';

describe('MarkupCommentService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MarkupCommentService]
    });
  });

  it('should ...', inject([MarkupCommentService], (service: MarkupCommentService) => {
    expect(service).toBeTruthy();
  }));
});
