import ToggleBtn from 'components/btns/ToggleBtn/ToggleBtn.tsx';
import styles from './Edit.module.css';
import { Route, Routes } from 'react-router-dom';
import EditProduct from './EditProduct/EditProduct.tsx';
import EditAddBtn from './EditAddBtn/EditAddBtn.tsx';
import AddProduct from './AddProduct/AddProduct.tsx';
import ChooseCharacteristics from './ChooseCharacteristics/ChooseCharacteristics.tsx';

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
        <Routes>
          <Route path={'products'} element={<EditAddBtn to={'addProduct'} />} />
        </Routes>
      </div>
      <Routes>
        <Route path={'products'} element={<EditProduct />} />
        <Route path={'characteristics'} element={<ChooseCharacteristics />} />
        <Route path={'products/addProduct'} element={<AddProduct />} />
      </Routes>
    </div>
  );
}

export default Edit;
