package com.example.pc.Services;

import com.example.pc.IServices.ICoolerService;
import com.example.pc.Models.ComputerCaseModel;
import com.example.pc.Models.CoolerModel;
import com.example.pc.repositories.CoolerRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class CoolerService implements ICoolerService {

    private final CoolerRepo coolerRepo;

    @Autowired
    public CoolerService(CoolerRepo coolerRepo) {
        this.coolerRepo = coolerRepo;
    }

    @Override
    public List<CoolerModel> getAllCoolers() {
        return coolerRepo.findAll();
    }

    @Override
    public CoolerModel getCoolerById(Long id) {
        return coolerRepo.findById(id).orElse(null);
    }

    @Override
    public CoolerModel addCooler(CoolerModel coolerModel) {
        return coolerRepo.save(coolerModel);
    }

    @Override
    public void deleteCoolerById(Long id) {
        coolerRepo.deleteById(id);
    }
}
