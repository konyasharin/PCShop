import useRadios from 'hooks/useRadios.ts';
import styles from './EditProduct.module.css';
import EComponentTypes from 'enums/EComponentTypes.ts';
import Input from 'components/Input/Input.tsx';
import { useState } from 'react';
import videoCardImg from 'assets/videocard.jpg';
import EditProductInfo from '../EditProductInfo/EditProductInfo.tsx';
import Btn from 'components/btns/Btn/Btn.tsx';
import CategoriesBlocks from '../CategoriesBlocks/CategoriesBlocks.tsx';

function EditProduct() {
  const { radios: categories, setRadioIsActive: setCategoryIsActive } =
    useRadios([
      { text: 'Видеокарты', isActive: true },
      { text: 'Процессоры', isActive: false },
    ]);
  const [productId, setProductId] = useState('');
  const [productName, setProductName] = useState('');
  const [price, setPrice] = useState('10000');
  const [activeCategory, setActiveCategory] = useState<EComponentTypes>(
    EComponentTypes.videoCard,
  );
  return (
    <div className={styles.block}>
      <h5 className={styles.title}>Категория</h5>
      <CategoriesBlocks
        categories={categories}
        setCategoryIsActive={setCategoryIsActive}
        setActiveCategory={setActiveCategory}
        className={styles.checkBoxes}
      />
      <Input
        value={productId}
        placeholder={'Введите id товара'}
        onChange={newValue => setProductId(newValue)}
        className={styles.input}
        type={'number'}
      />
      <EditProductInfo
        type={activeCategory}
        productName={productName}
        setProductName={setProductName}
        price={price}
        setPrice={setPrice}
        img={videoCardImg}
      />
      <Btn>Редактировать</Btn>
      <h5 className={styles.delete}>Удалить товар</h5>
    </div>
  );
}

export default EditProduct;
