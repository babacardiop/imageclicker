import { Component, NgZone } from '@angular/core';
import { SignalRService } from '../services/signalr.service';
import { Tab } from '../models/tab.model';
import { ImageMessage } from '../models/imgmessage.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  chatMessage: ImageMessage;
  canSendMessage: boolean;
  tabs: Tab[];
  currentRoom: string;

  constructor(
    private signalrService: SignalRService,
    private _ngZone: NgZone
  ) {
    this.subscribeToEvents();
    this.chatMessage = new ImageMessage();
    this.tabs = [];
    this.tabs.push(new Tab('Lobby', 'Welcome to lobby'));
    this.tabs.push(new Tab('SignalR', 'Welcome to SignalR Room'));
    this.currentRoom = 'Lobby';
  }

  sendMessage() {
    if (this.canSendMessage) {
      this.chatMessage.room = this.currentRoom;
      this.signalrService.sendChatMessage(this.chatMessage);
    }
  }

  onRoomChange(room) {
    this.currentRoom = room;
  }

  private subscribeToEvents(): void {
    this.signalrService.connectionEstablished.subscribe(() => {
      this.canSendMessage = true;
    });

    this.signalrService.messageReceived.subscribe((message: ImageMessage) => {
      this._ngZone.run(() => {
        this.chatMessage = new ImageMessage();
        const room = this.tabs.find(t => t.heading === message.room);
        if (room) {
          room.messageHistory.push(new ImageMessage(message.user, message.message, message.room));
        }
      });
    });
  }
}
