import { Rickyportal2Page } from './app.po';

describe('rickyportal2 App', function() {
  let page: Rickyportal2Page;

  beforeEach(() => {
    page = new Rickyportal2Page();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
