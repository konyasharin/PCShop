import useRadios from 'hooks/useRadios.ts';
import styles from './EditProduct.module.css';
import EComponentTypes from 'enums/EComponentTypes.ts';
import Input from 'components/Input/Input.tsx';
import React, { useEffect, useRef, useState } from 'react';
import emptyImg from 'assets/empty-img.png';
import EditProductInfo from '../EditProductInfo/EditProductInfo.tsx';
import Btn from 'components/btns/Btn/Btn.tsx';
import CategoriesBlocks from '../CategoriesBlocks/CategoriesBlocks.tsx';
import useFilters from 'hooks/useFilters.ts';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import useBorderValues from 'hooks/useBorderValues.ts';

function EditProduct() {
  const { radios: categories, setRadioIsActive: setCategoryIsActive } =
    useRadios([
      { text: 'Видеокарты', isActive: true },
      { text: 'Процессоры', isActive: false },
    ]);
  const [productId, setProductId] = useState('');
  const [productName, setProductName] = useState('');
  const [price, setPrice] = useBorderValues(1, 1);
  const imgFile: React.MutableRefObject<null | File> = useRef(null);
  const [description, setDescription] = useState('');
  const [amount, setAmount] = useBorderValues(0, 0);
  const [power, setPower] = useBorderValues(0, 0, 10);
  const allFiltersState = useSelector((state: RootState) => state.filters);
  const [activeCategory, setActiveCategory] = useState<EComponentTypes>(
    EComponentTypes.videoCard,
  );
  const [img, setImg] = useState(emptyImg);
  const { filters, setRadioIsActive, setComponentTypeHandle } = useFilters(
    allFiltersState,
    'radio',
    activeCategory,
  );
  useEffect(() => {
    setComponentTypeHandle(activeCategory);
  }, [activeCategory]);
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
        img={img}
        setImg={setImg}
        imgFileRef={imgFile}
        setDescription={setDescription}
        description={description}
        amount={amount}
        setAmount={setAmount}
        power={power}
        setPower={setPower}
        filters={filters}
        setRadioIsActive={setRadioIsActive}
      />
      <Btn>Редактировать</Btn>
      <h5 className={styles.delete}>Удалить товар</h5>
    </div>
  );
}

export default EditProduct;
