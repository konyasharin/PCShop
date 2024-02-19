package com.example.pc.IServices;

import com.example.pc.Models.CoolerModel;
import com.example.pc.dto.CoolerDto;

import java.util.List;

public interface ICoolerService {

    List<CoolerModel> getAllCoolers();
    CoolerModel getCoolerById(Long id);
    CoolerModel updateCooler(CoolerModel coolerModel);
    void deleteCoolerById(Long id);
    CoolerModel createCooler(CoolerDto coolerDto);
}
