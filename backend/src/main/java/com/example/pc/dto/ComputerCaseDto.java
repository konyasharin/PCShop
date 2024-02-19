package com.example.pc.dto;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;


@Data
public class ComputerCaseDto {

    private String brand;

    private String model;

    private String description;

    private Integer price;

    private String country;

    private String material;

    private Integer width;

    private Integer height;

    private Integer depth;

    private MultipartFile image;
}
