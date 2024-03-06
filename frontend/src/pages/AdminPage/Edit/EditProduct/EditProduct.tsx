import useRadios from 'hooks/useRadios.ts';
import Radio from 'components/Radio/Radio.tsx';
import styles from './EditProduct.module.css';
import EComponentTypes from 'enums/EComponentTypes.ts';
import Input from 'components/Input/Input.tsx';
import { useState } from 'react';
import videoCardImg from 'assets/videocard.jpg';
import EditFilterBlock from '../EditFilterBlock/EditFilterBlock.tsx';
import EditProductInfo from '../EditProductInfo/EditProductInfo.tsx';

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

  const categoriesBlocks = categories.map((category, i) => {
    return (
      <Radio
        text={category.text}
        isActive={category.isActive}
        onChange={() => {
          setCategoryIsActive(i);
          switch (category.text) {
            case 'Видеокарты':
              setActiveCategory(EComponentTypes.videoCard);
              break;
            case 'Процессоры':
              setActiveCategory(EComponentTypes.processor);
              break;
          }
        }}
        className={styles.checkBox}
      />
    );
  });
  return (
    <div className={styles.block}>
      <h5 className={styles.title}>Категория</h5>
      <div className={styles.checkBoxes}>{...categoriesBlocks}</div>
      <Input
        value={productId}
        placeholder={'Введите id товара'}
        onChange={newValue => setProductId(newValue)}
        className={styles.input}
        type={'number'}
      />
      <Input
        value={productName}
        placeholder={'Введите название товара'}
        onChange={newValue => setProductName(newValue)}
        className={styles.input}
      />
      <img src={videoCardImg} alt="videoCard" />
      <EditFilterBlock
        title={'Цена'}
        filterBlock={
          <Input
            value={price}
            placeholder={'Цена товара'}
            onChange={newValue => setPrice(newValue)}
            className={styles.priceInput}
            type={'number'}
          />
        }
        className={styles.filterBlock}
      />
      <EditProductInfo type={activeCategory} />
    </div>
  );
}

export default EditProduct;
