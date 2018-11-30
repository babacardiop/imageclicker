/** represent chat message class */
export class ImageMessage {

  user: string;
  message: string;
  room: string;

  constructor(user: string = '', message: string = '', room: string = '') {
    this.user = user;
    this.message = message;
    this.room = room;
  }
}
