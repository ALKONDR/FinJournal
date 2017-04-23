import { observer } from 'mobx-react';
import React from 'react';
import api from '../utils/api';
import LoginState from './LoginState';

const NavLink = require('react-router-dom').NavLink;

@observer
class Nav extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      popularTopics: api.getPopularTopics(),
    };
  }

  render() {
    if (this.state.popularTopics[0] !== 'Subscriptions' && LoginState.userLoggedIn) {
      this.state.popularTopics.unshift('Subscriptions');
    }

    if (this.state.popularTopics[0] === 'Subscriptions' && !LoginState.userLoggedIn) {
      this.state.popularTopics.splice(0, 1);
    }

    return (
      <ul className="nav">
        {this.state.popularTopics.map(topic => (
          <li key={topic} className="navEl">
            <NavLink exact activeClassName="activeNavLink" to={`/topic/${topic}`} >
              {topic}
            </NavLink>
          </li>
          ),
        )}
      </ul>
    );
  }
}

module.exports = Nav;
