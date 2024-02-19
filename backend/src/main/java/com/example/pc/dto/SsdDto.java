package com.example.pc.dto;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;

@Data
public class SsdDto {

    private String brand;

    private String model;

    private Integer price;

    private String description;

    private String country;

    private Integer capacity;

    private MultipartFile image;
}
