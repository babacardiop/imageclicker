import { ImageMessage } from './imgmessage.model';

/** Represent Tab class */
export class Tab {
  messageHistory: ImageMessage[];
  heading: string;
  title: string;

  constructor(
    heading: string = '',
    title: string = ''
  ) {
    this.heading = heading;
    this.title = title;
    this.messageHistory = [];
  }
}
