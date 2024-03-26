import useRadios from 'hooks/useRadios.ts';
import styles from './EditProduct.module.css';
import componentTypes from 'enums/componentTypes.ts';
import React, { useEffect, useRef, useState } from 'react';
import emptyImg from 'assets/empty-img.png';
import EditProductInfo from '../EditProductInfo/EditProductInfo.tsx';
import Btn from 'components/btns/Btn/Btn.tsx';
import CategoriesBlocks from '../CategoriesBlocks/CategoriesBlocks.tsx';
import useFilters from 'hooks/useFilters.ts';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import useBorderValues from 'hooks/useBorderValues.ts';
import SearchInput from 'components/inputs/SearchInput/SearchInput.tsx';
import getComponent from 'api/components/getComponent.ts';

function EditProduct() {
  const { radios: categories, setRadioIsActive: setCategoryIsActive } =
    useRadios([
      { text: 'Видеокарты', isActive: true },
      { text: 'Процессоры', isActive: false },
    ]);
  const [productId, setProductId] = useState('');
  const [price, setPrice] = useBorderValues(1, 1);
  const imgFile: React.MutableRefObject<null | File> = useRef(null);
  const [description, setDescription] = useState('');
  const [amount, setAmount] = useBorderValues(0, 0);
  const [power, setPower] = useBorderValues(0, 0, 10);
  const allFiltersState = useSelector((state: RootState) => state.filters);
  const [activeCategory, setActiveCategory] =
    useState<keyof typeof componentTypes>('videoCard');
  const [img, setImg] = useState(emptyImg);
  const {
    filters,
    setRadioIsActive,
    setComponentTypeHandle,
    findIndexOfFilter,
  } = useFilters(allFiltersState, 'radio', activeCategory);
  const [productIsGot, setProductIsGot] = useState(false);
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
      <SearchInput
        value={productId}
        placeholder={'Введите id товара'}
        onChange={newValue => setProductId(newValue)}
        className={styles.input}
        type={'number'}
        onSearch={() => {
          getComponent(activeCategory, +productId).then(response => {
            setPrice(response.data.price);
            setImg(response.data.image);
            setDescription(response.data.description);
            setAmount(response.data.amount);
            setPower(response.data.power);
            let key: keyof typeof filters;
            for (key in filters) {
              const index = findIndexOfFilter(key, response.data[key]);
              if (index) {
                setRadioIsActive(key, index);
              }
            }
          });
        }}
      />
      <div style={{ display: productIsGot ? 'block' : 'none' }}>
        <EditProductInfo
          type={activeCategory}
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
    </div>
  );
}

export default EditProduct;
