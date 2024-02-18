package com.example.pc.Services;

import com.example.pc.IServices.IComputerCaseService;
import com.example.pc.Models.ComputerCaseModel;
import com.example.pc.repositories.ComputerCaseRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@Service
public class ComputerCaseService implements IComputerCaseService {

    private final ComputerCaseRepo computerCaseRepo;

    @Autowired
    public ComputerCaseService(ComputerCaseRepo computerCaseRepo) {
        this.computerCaseRepo = computerCaseRepo;
    }

    @Override
    public List<ComputerCaseModel> getAllComputerCases() {
        return computerCaseRepo.findAll();
    }

    @Override
    public ComputerCaseModel getComputerCaseById(Long id) {
        return computerCaseRepo.findById(id).orElse(null);
    }

    @Override
    public ComputerCaseModel addComputerCase(ComputerCaseModel computerCaseModel) {
        return computerCaseRepo.save(computerCaseModel);
    }

    @Override
    public void deleteComputerCaseById(Long id) {
        computerCaseRepo.deleteById(id);
    }

}
