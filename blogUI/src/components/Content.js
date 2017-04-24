/* eslint-disable no-console */

import React from 'react';
import PropTypes from 'prop-types';
import { observer } from 'mobx-react';
import Nav from './Nav';
import Preview from './Preview';
import api from '../utils/api';
import LoginState from './LoginState';

@observer
class Content extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      previews: !this.props.match.params.topic ? api.getPopularArticles() : [],
    };

    this.setPreviews = this.setPreviews.bind(this);
    this.getData = this.getData.bind(this);
  }

  getData() {
    if (this.props.match.params.topic === 'subscriptions') {
      api.getSubscriptions()
        .then((response) => {
          if (response.status >= 200 && response.status < 300) {
            this.setPreviews(response.status === 204 ? [] : response.data);
          }
        })
        .catch(() => {
          api.refreshToken()
            .then((refreshed) => {
              if (refreshed) {
                api.getSubscriptions()
                  .then((response) => {
                    if ((response.status >= 200) && (response.status < 300)) {
                      this.setPreviews(response.status === 204 ? [] : response.data);
                    }
                  });
              } else {
                LoginState.userLoggedIn = false;
              }
            })
            .catch((error) => { console.log(error); });
        });
    } else if (this.props.match.params.topic) {
      api.getArticlesByTag(this.props.match.params.topic)
        .then((response) => {
          this.setPreviews(response.status === 204 ? [] : response.data);
        });
    }
  }

  setPreviews(data) {
    console.log(data);
    if (!this.props.match.params.topic) {
      this.state.previews = api.getPopularArticles();
    } else {
      this.state.previews = [];
    }
  }

  render() {
    this.getData();
    return (
      <div className="content">
        <Nav />
        {this.state.previews.map(preview => <Preview previewData={preview} />)}
      </div>
    );
  }
}

Content.defaultProps = {
  match: {
    params: {
      topic: '',
    },
  },
};

Content.propTypes = {
  match: PropTypes.shape({
    params: PropTypes.shape({
      topic: PropTypes.string,
    }),
  }),
};

module.exports = Content;
