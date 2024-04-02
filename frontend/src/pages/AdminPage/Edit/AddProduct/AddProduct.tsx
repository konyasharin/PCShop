import CategoriesBlocks from '../CategoriesBlocks/CategoriesBlocks.tsx';
import useRadios from 'hooks/useRadios.ts';
import React, { useContext, useEffect, useRef, useState } from 'react';
import componentTypes from 'enums/componentTypes.ts';
import styles from './AddProduct.module.css';
import EditProductInfo from '../EditProductInfo/EditProductInfo.tsx';
import emptyImg from 'assets/empty-img.png';
import Btn from 'components/btns/Btn/Btn.tsx';
import createComponent from 'api/components/createComponent.ts';
import useFilters from 'hooks/useFilters.ts';
import TCheckBox from 'types/TCheckBox.ts';
import useBorderValues from 'hooks/useBorderValues.ts';
import initCategories from '../initCategories.ts';
import { FiltersContext } from '../../../../App.tsx';

function searchActiveFromFilter(filter: TCheckBox[]): TCheckBox | null {
  let result: null | TCheckBox = null;
  filter.forEach(filterElem => {
    if (filterElem.isActive) {
      result = filterElem;
    }
  });
  return result;
}

function AddProduct() {
  const allFilters = useContext(FiltersContext);
  const { radios: categories, setRadioIsActive: setCategoryIsActive } =
    useRadios(initCategories());
  const [activeCategory, setActiveCategory] =
    useState<keyof typeof componentTypes>('videoCard');
  const [price, setPrice] = useBorderValues(1, 1);
  const [img, setImg] = useState(emptyImg);
  const [description, setDescription] = useState('');
  const [amount, setAmount] = useBorderValues(0, 0);
  const [power, setPower] = useBorderValues(0, 0, 10);
  const imgFile: React.MutableRefObject<null | File> = useRef(null);
  const { filters, setRadioIsActive, setComponentType } = useFilters(
    'radio',
    activeCategory,
    allFilters,
  );
  useEffect(() => {
    setComponentType(activeCategory);
  }, [activeCategory]);

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
        price={price}
        setPrice={setPrice}
        setImg={setImg}
        description={description}
        setDescription={setDescription}
        amount={amount}
        setAmount={setAmount}
        power={power}
        setPower={setPower}
        filters={filters}
        setRadioIsActive={setRadioIsActive}
        imgFileRef={imgFile}
      />
      <Btn
        onClick={() => {
          if (filters) {
            const filtersObject = Object.keys(filters).reduce(
              (acc, key) => {
                const active = searchActiveFromFilter(
                  filters[key as keyof typeof filters],
                );
                acc[key as keyof typeof filters] = active ? active.text : '';
                return acc;
              },
              {} as { [key in keyof typeof filters]: string },
            );
            if (imgFile.current != null) {
              const requestData = {
                ...filtersObject,
                country: 'Russia',
                price: price,
                description: description,
                image: imgFile.current,
                amount: amount,
                power: power,
              };
              void createComponent(activeCategory, requestData);
            }
          }
        }}
      >
        Создать товар
      </Btn>
    </div>
  );
}

export default AddProduct;
