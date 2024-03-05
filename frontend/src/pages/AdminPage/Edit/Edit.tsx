import ToggleBtn from 'components/btns/ToggleBtn/ToggleBtn.tsx';
import styles from './Edit.module.css';
import { Route, Routes } from 'react-router-dom';
import EditProduct from './EditProduct/EditProduct.tsx';

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
      <Routes>
        <Route path={'products'} element={<EditProduct />} />
        <Route path={'characteristics'} />
      </Routes>
    </div>
  );
}

export default Edit;
