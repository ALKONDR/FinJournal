/* eslint-disable jsx-a11y/no-static-element-interactions */

import { observer } from 'mobx-react';
import React from 'react';
import PropTypes from 'prop-types';

@observer
class Search extends React.Component {
  constructor(props) {
    super(props);

    this.toggleInput = this.toggleInput.bind(this);
  }

  toggleInput() {
    this.props.headerState.opened = !this.props.headerState.opened;
  }

  render() {
    const { opened } = this.props.headerState;
    return (
      <div className="search">
        <input
          type="text"
          className={opened ? 'searchInput inputOpened' : 'searchInput inputClosed'}
          placeholder="search"
        />
        <img
          role="button"
          onClick={this.toggleInput}
          className="searchIcon"
          alt="searh"
        />
      </div>
    );
  }
}

Search.propTypes = {
  headerState: PropTypes.shape({
    opened: PropTypes.bool.isRequired,
  }).isRequired,
};

module.exports = Search;
