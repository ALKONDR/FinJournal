import { observable } from 'mobx';

class HeaderState {
  @observable inputOpened = false;
}

module.exports = new HeaderState();
