import useRadios from 'hooks/useRadios.ts';
import styles from './EditProduct.module.css';
import componentTypes from 'enums/componentTypes.ts';
import React, { useContext, useEffect, useRef, useState } from 'react';
import emptyImg from 'assets/empty-img.png';
import EditProductInfo from '../EditProductInfo/EditProductInfo.tsx';
import Btn from 'components/btns/Btn/Btn.tsx';
import CategoriesBlocks from '../CategoriesBlocks/CategoriesBlocks.tsx';
import useFilters from 'hooks/useFilters.ts';
import useBorderValues from 'hooks/useBorderValues.ts';
import SearchInput from 'components/inputs/SearchInput/SearchInput.tsx';
import getComponent from 'api/components/getComponent.ts';
import initCategories from '../initCategories.ts';
import { FiltersContext } from '../../../../App.tsx';

function EditProduct() {
  const allFilters = useContext(FiltersContext);
  const { radios: categories, setRadioIsActive: setCategoryIsActive } =
    useRadios(initCategories());
  const [productId, setProductId] = useState('');
  const [price, setPrice] = useBorderValues(1, 1);
  const imgFile: React.MutableRefObject<null | File> = useRef(null);
  const [description, setDescription] = useState('');
  const [amount, setAmount] = useBorderValues(0, 0);
  const [power, setPower] = useBorderValues(0, 0, 10);
  const [activeCategory, setActiveCategory] =
    useState<keyof typeof componentTypes>('videoCard');
  const [img, setImg] = useState(emptyImg);
  const { filters, setRadioIsActive, setComponentType, findIndexOfFilter } =
    useFilters('radio', activeCategory, allFilters);
  const [productIsGot, setProductIsGot] = useState(false);
  useEffect(() => {
    setComponentType(activeCategory);
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
            if (filters) {
              let key: keyof typeof filters;
              for (key in filters) {
                const index = findIndexOfFilter(key, response.data[key]);
                if (index) {
                  setRadioIsActive(key, index);
                }
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
