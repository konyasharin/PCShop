import CategoriesBlocks from '../CategoriesBlocks/CategoriesBlocks.tsx';
import useRadios from 'hooks/useRadios.ts';
import { useState } from 'react';
import EComponentTypes from 'enums/EComponentTypes.ts';
import styles from './AddProduct.module.css';
import EditProductInfo from '../EditProductInfo/EditProductInfo.tsx';
import videoCardImg from 'assets/videocard.jpg';
import Btn from 'components/btns/Btn/Btn.tsx';

function AddProduct() {
  const { radios: categories, setRadioIsActive: setCategoryIsActive } =
    useRadios([
      { text: 'Видеокарты', isActive: true },
      { text: 'Процессоры', isActive: false },
    ]);
  const [activeCategory, setActiveCategory] = useState<EComponentTypes>(
    EComponentTypes.videoCard,
  );
  const [productName, setProductName] = useState('');
  const [price, setPrice] = useState('10000');

  return (
    <div className={styles.block}>
      <CategoriesBlocks
        categories={categories}
        setCategoryIsActive={setCategoryIsActive}
        setActiveCategory={setActiveCategory}
        className={styles.checkBoxes}
      />
      <EditProductInfo
        type={activeCategory}
        img={videoCardImg}
        productName={productName}
        setProductName={setProductName}
        price={price}
        setPrice={setPrice}
      />
      <Btn>Создать товар</Btn>
    </div>
  );
}

export default AddProduct;
