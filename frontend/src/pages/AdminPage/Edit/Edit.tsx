import ToggleBtn from 'components/btns/ToggleBtn/ToggleBtn.tsx';
import styles from './Edit.module.css';

function Edit() {
  return (
    <div>
      <div className={styles.toggleBtns}>
        <ToggleBtn className={styles.toggleBtn} to={'characteristics'}>
          Характеристики
        </ToggleBtn>
        <ToggleBtn className={styles.toggleBtn} to={'products'}>
          Товары
        </ToggleBtn>
      </div>
    </div>
  );
}

export default Edit;
