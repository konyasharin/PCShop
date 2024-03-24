import styles from './AddComment.module.css';
import { useState } from 'react';
import Btn from 'components/btns/Btn/Btn.tsx';
import Stars from '../Stars/Stars.tsx';

function AddComment() {
  const [mark, setMark] = useState(1);
  const [comment, setComment] = useState('');
  return (
    <div className={styles.addComment}>
      <div className={styles.topBlock}>
        <textarea
          placeholder={'Ваш комментарий'}
          value={comment}
          onChange={e => setComment(e.target.value)}
        />
        <Stars mark={mark} setMark={setMark} className={styles.stars} />
      </div>
      <Btn>Отправить</Btn>
    </div>
  );
}

export default AddComment;
