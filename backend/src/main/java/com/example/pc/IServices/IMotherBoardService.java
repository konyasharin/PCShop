package com.example.pc.IServices;

import com.example.pc.Models.MotherBoardModel;

import java.util.List;

public interface IMotherBoardService {

    List<MotherBoardModel> getMotherBoards();
    MotherBoardModel getMotherBoardById(Long id);
    MotherBoardModel addMotherBoard(MotherBoardModel motherBoardModel);
    void deleteMotherBoardById(Long id);
}
