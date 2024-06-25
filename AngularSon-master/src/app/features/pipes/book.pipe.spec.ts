import { BookPipe } from './book.pipe';

describe('BookPipe', () => {
  it('create an instance', () => {
    const pipe = new BookPipe();
    expect(pipe).toBeTruthy();
  });
});
