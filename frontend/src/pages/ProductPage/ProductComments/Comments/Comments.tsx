import FiltersPanel from 'components/FiltersPanel/FiltersPanel.tsx';
import useCheckBoxes from 'hooks/useCheckBoxes.ts';
import CheckBox from 'components/CheckBox/CheckBox.tsx';
import styles from './Comments.module.css';
import Comment from './Comment/Comment.tsx';

function Comments() {
  const { checkBoxes, setCheckBoxIsActive } = useCheckBoxes([
    { text: '1', isActive: false },
    { text: '2', isActive: false },
    { text: '3', isActive: false },
    { text: '4', isActive: false },
    { text: '5', isActive: false },
  ]);
  const checkBoxesBlocks = checkBoxes.map((checkBox, i) => {
    return (
      <CheckBox
        text={checkBox.text}
        isActive={checkBox.isActive}
        onChange={() => setCheckBoxIsActive(i, !checkBox.isActive)}
        className={styles.checkBox}
      />
    );
  });
  const filterBlock = (
    <div>
      <h6 className={styles.filterTitle}>Оценка</h6>
      {...checkBoxesBlocks}
    </div>
  );
  return (
    <div className={styles.comments}>
      <FiltersPanel blocks={[filterBlock]} className={styles.filtersPanel} />
      <div>
        <Comment
          comment={
            'Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий  Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий  Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий '
          }
          mark={4}
          className={styles.comment}
        />
        <Comment
          comment={
            'Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий  Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий  Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий Здесь комментарий '
          }
          mark={1}
          className={styles.comment}
        />
      </div>
    </div>
  );
}

export default Comments;
