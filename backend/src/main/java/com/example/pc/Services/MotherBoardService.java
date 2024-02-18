package com.example.pc.Services;

import com.example.pc.IServices.IMotherBoardService;
import com.example.pc.Models.CoolerModel;
import com.example.pc.Models.MotherBoardModel;
import com.example.pc.repositories.MotherBoardRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class MotherBoardService implements IMotherBoardService {

    private final MotherBoardRepo motherBoardRepo;

    @Autowired
    public MotherBoardService(MotherBoardRepo motherBoardRepo) {
        this.motherBoardRepo = motherBoardRepo;
    }

    @Override
    public List<MotherBoardModel> getMotherBoards() {
        return motherBoardRepo.findAll();
    }

    @Override
    public MotherBoardModel getMotherBoardById(Long id) {
        return motherBoardRepo.findById(id).orElse(null);
    }

    @Override
    public MotherBoardModel addMotherBoard(MotherBoardModel motherBoardModel) {
        return motherBoardRepo.save(motherBoardModel);
    }

    @Override
    public void deleteMotherBoardById(Long id) {
        motherBoardRepo.deleteById(id);
    }
}
