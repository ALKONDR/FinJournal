/* eslint-disable prefer-template */

import { observer } from 'mobx-react';
import React from 'react';
import PropTypes from 'prop-types';
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
    if (this.state.popularTopics[0] !== 'subscriptions' && LoginState.userLoggedIn) {
      this.state.popularTopics.unshift('subscriptions');
    }

    if (this.state.popularTopics[0] === 'subscriptions' && !LoginState.userLoggedIn) {
      this.state.popularTopics.splice(0, 1);
    }

    const currentTopic = this.props.current || 'popular';
    return (
      <ul className="nav">
        {this.state.popularTopics.map(topic => (
          <li key={topic} className={'navEl' + (currentTopic === topic ? ' activeNavLink' : '')}>
            <NavLink
              exact
              to={topic === 'popular' ? '/' : `/topic/${topic}`}
            >
              {topic}
            </NavLink>
          </li>
          ),
        )}
      </ul>
    );
  }
}

Nav.propTypes = {
  current: PropTypes.string.isRequired,
};

module.exports = Nav;
