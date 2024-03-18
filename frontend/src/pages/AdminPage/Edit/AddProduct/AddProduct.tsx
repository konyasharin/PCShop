import CategoriesBlocks from '../CategoriesBlocks/CategoriesBlocks.tsx';
import useRadios from 'hooks/useRadios.ts';
import { useState } from 'react';
import EComponentTypes from 'enums/EComponentTypes.ts';
import styles from './AddProduct.module.css';
import EditProductInfo from '../EditProductInfo/EditProductInfo.tsx';
import emptyImg from 'assets/empty-img.png';
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
  const [img, setImg] = useState(emptyImg);

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
        img={img}
        productName={productName}
        setProductName={setProductName}
        price={price}
        setPrice={setPrice}
        onChangeImg={e => {
          if (e.target.files) {
            setImg(URL.createObjectURL(e.target.files[0]));
          }
        }}
      />
      <Btn>Создать товар</Btn>
    </div>
  );
}

export default AddProduct;
