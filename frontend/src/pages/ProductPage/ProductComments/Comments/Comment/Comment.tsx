import React from 'react';
import Stars from '../../Stars/Stars.tsx';
import styles from './Comment.module.css';
import binImg from 'assets/TrashBin.png';
import createClassNames from 'utils/createClassNames.ts';

type CommentProps = {
  comment: string;
  mark: number;
  className?: string;
};

const Comment: React.FC<CommentProps> = props => {
  return (
    <div className={createClassNames([styles.comment, props.className])}>
      <Stars mark={props.mark} />
      <div className={styles.commentText}>{props.comment}</div>
      <img src={binImg} alt="bin" className={styles.bin} />
    </div>
  );
};

export default Comment;
