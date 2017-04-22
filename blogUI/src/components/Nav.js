import React from 'react';
import api from '../utils/api';

class Nav extends React.Component {
  render() {
    const popularTopics = api.getPopularTopics();
    return (
      <ul className="nav">
        {popularTopics.map(topic => <li key={topic} className="navEl">{topic}</li>)}
      </ul>
    );
  }
}

module.exports = Nav;
