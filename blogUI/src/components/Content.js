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

  componentWillMount() {
    this.getData();
  }

  componentWillReceiveProps() {
    console.log(this.props.match);
    this.getData();
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
                    if (response.status >= 200 && response.status < 300) {
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
    } else {
      this.setPreviews();
    }
  }

  setPreviews(data) {
    this.setState({
      previews: [],
    });

    if (!this.props.match.params.topic) {
      this.setState({
        previews: api.getPopularArticles(),
      });
    } else {
      this.setState({
        previews: data.map((element) => {
          const preview = {
            username: element.author,
            date: {
              day: Date(element.date).split(' ')[2],
              month: Date(element.date).split(' ')[1],
            },
            readingTime: element.readingTime,
            caption: element.title,
            description: element.description || 'no description provided',
            likes: element.likes.length,
            dislikes: element.dislikes.length,
            comments: element.comments.length,
          };

          return preview;
        }),
      });
    }
  }

  render() {
    return (
      <div className="content">
        <Nav />
        { this.state.previews.length > 0 ?
            this.state.previews.map(preview => <Preview previewData={preview} />) :
            <div>No content</div>
        }
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
